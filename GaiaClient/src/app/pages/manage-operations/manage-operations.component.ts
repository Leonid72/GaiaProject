import { Component, DestroyRef, inject } from '@angular/core';
import { OperationService } from '../../services/operation.service';
import { takeUntilDestroyed, toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-manage-operations',
  imports: [RouterLink],
  templateUrl: './manage-operations.component.html',
  styleUrl: './manage-operations.component.css'
})
export class ManageOperationsComponent {


  private operationService = inject(OperationService);
  private destroyRef = inject(DestroyRef);

  operations = toSignal(this.operationService. getAll(), { initialValue: [] });
  private toastr = inject(ToastrService)

  toggleStatus(op: any) {
  const newStatus = !op.isActive;
  this.operationService.updateStatus(op.id, newStatus)
  .pipe(takeUntilDestroyed(this.destroyRef))
  .subscribe({
    next: () => {
      op.isActive = newStatus;
      // Optional: Add a toast notification here
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
