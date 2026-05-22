import { Component, DestroyRef, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { Subject, switchMap } from 'rxjs';
import { OperationService } from '../../services/operation.service';
import { OperationExecuteRequestDto, OperationExecuteResponseDto } from '../../models/operation.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-calculator',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent {

  private fb = inject(FormBuilder);
  private operationService = inject(OperationService);
  private destroyRef = inject(DestroyRef);

  operations = toSignal(this.operationService.activeOperations$, { initialValue: [] });
  calculationState = signal<OperationExecuteResponseDto | null>(null);
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);

  calcForm = this.fb.group({
    operationName: ['', Validators.required],
    fieldA: ['', Validators.required],
    fieldB: ['', Validators.required]
  });

  get operationName() { return this.calcForm.get('operationName'); }
  get fieldA() { return this.calcForm.get('fieldA'); }
  get fieldB() { return this.calcForm.get('fieldB'); }

  private calculateSubject = new Subject<OperationExecuteRequestDto>();

  constructor() {
    this.calculateSubject.pipe(
      switchMap(payload => {
        this.isLoading.set(true);
        this.errorMessage.set(null);
        return this.operationService.execute(payload);
      }),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.calculationState.set(res);
        } else {
          this.errorMessage.set(res.errorMessage || 'Invalid input');
          this.calculationState.set(null);
        }
        this.isLoading.set(false);
      },
      error: (err) => {
        const apiError = err.error?.errorMessage || 'Server Error: Invalid data provided';
        this.errorMessage.set(apiError);
        this.calculationState.set(null);
        this.isLoading.set(false);
      }
    });
  }

  onCalculate() {
    if (this.calcForm.invalid) return;
    this.calculateSubject.next(this.calcForm.getRawValue() as OperationExecuteRequestDto);
  }

  onClear() {
    this.calcForm.reset({ operationName: '' });
    this.calculationState.set(null);
  }
}
