import { Component, DestroyRef, inject, signal } from '@angular/core';
import { OperationService } from '../../services/operation.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OperationDto } from '../../models/operation.model';

@Component({
  selector: 'app-manage-operations',
  imports: [RouterLink],
  templateUrl: './manage-operations.component.html',
  styleUrl: './manage-operations.component.css'
})
export class ManageOperationsComponent {

  private operationService = inject(OperationService);
  private destroyRef = inject(DestroyRef);
  private toastr = inject(ToastrService);

  private _operations = signal<OperationDto[]>([]);
  readonly operations = this._operations.asReadonly();

  constructor() {
    this.operationService.getAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(ops => this._operations.set(ops));
  }

  toggleStatus(op: OperationDto) {
    const newStatus = !op.isActive;
    this.operationService.updateStatus(op.id, newStatus)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this._operations.update(ops =>
            ops.map(o => o.id === op.id ? { ...o, isActive: newStatus } : o)
          );
          const action = newStatus ? 'activated' : 'deactivated';
          this.toastr.success(`Operation "${op.name}" has been ${action}.`, 'Success');
        },
        error: (err) => {
          console.error('Failed to update status', err);
          this.toastr.error(`Failed to update status for operation "${op.name}".`, 'Error');
        }
      });
  }
}
