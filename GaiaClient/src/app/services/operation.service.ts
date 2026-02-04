import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { CreateOperationDto, OperationDetailsDto, OperationDto, OperationExecuteRequestDto, OperationExecuteResponseDto } from '../models/operation.model';

@Injectable({
  providedIn: 'root'
})
export class OperationService {

  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiBaseUrl;

  // Calculator dropdown: active only
  readonly activeOperations$: Observable<OperationDto[]> =
    this.http.get<OperationDto[]>(`${this.baseUrl}/api/Operations/active`);


  // Manage page: all
  getAll(): Observable<OperationDto[]> {
    return this.http.get<OperationDto[]>(`${this.baseUrl}/api/Operations`);
  }

  // Optional details (if needed for update payload)
  getById(id: number): Observable<OperationDetailsDto> {
    return this.http.get<OperationDetailsDto>(`${this.baseUrl}/api/Operations/${id}`);
  }

  execute(req: OperationExecuteRequestDto): Observable<OperationExecuteResponseDto> {
    return this.http.post<OperationExecuteResponseDto>(`${this.baseUrl}/api/Operations/execute`, req);
  }

   // Optional: create new operation
  update(id: number, dto: CreateOperationDto & { isActive?: boolean }): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/api/Operations/${id}`, dto);
  }

  updateStatus(id: number, isActive: boolean): Observable<void> {
  // Matches your PATCH /api/Operations/{id}/status endpoint
  return this.http.patch<void>(`${this.baseUrl}/api/Operations/${id}/status`, { isActive });
}
  
}
