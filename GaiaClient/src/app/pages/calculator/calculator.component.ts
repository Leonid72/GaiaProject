import { Component, DestroyRef, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { OperationService } from '../../services/operation.service';
import { OperationExecuteResponseDto } from '../../models/operation.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-calculator',
  imports: [CommonModule, ReactiveFormsModule,RouterLink],
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent {
  
  private fb = inject(FormBuilder);
  private operationService = inject(OperationService);

  // Signals for state
  operations = toSignal(this.operationService.activeOperations$, { initialValue: [] });
  calculationState = signal<OperationExecuteResponseDto | null>(null);
  isLoading = signal(false);

  errorMessage = signal<string | null>(null);
  private destroyRef = inject(DestroyRef);
  
  calcForm = this.fb.group({
    operationName: ['', Validators.required],
    fieldA: ['', Validators.required],
    fieldB: ['', Validators.required]
  });

  onCalculate() {
    if (this.calcForm.invalid) return;

    this.isLoading.set(true);
    this.errorMessage.set(null);
    const payload = this.calcForm.getRawValue() as any;

    this.operationService.execute(payload)
    .pipe(takeUntilDestroyed(this.destroyRef)) // This is the critical part to prevent memory leaks 
    .subscribe({
      next: (res) => {
     if (res.isSuccess) {
        this.calculationState.set(res);
      } else {
        // Soft error (200 OK but application error)
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

  onClear() {
    this.calcForm.reset({ operationName: '' });
    this.calculationState.set(null);
  }

}
