import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DepartmentDto } from 'src/app/models/department/departmentDto';
import { AddDepartmentDto } from 'src/app/models/department/addDepartmentDto';
import { UpdateDepartmentDto } from 'src/app/models/department/updateDepartmentDto';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getall(): Observable<DepartmentDto[]> {
    return this.http.get<DepartmentDto[]>(`${this.apiUrl}/api/Department/`);
  }

  add(newUser: AddDepartmentDto): Observable<AddDepartmentDto> {
    return this.http.post<AddDepartmentDto>(`${this.apiUrl}/api/Department/`, newUser);
  }

  update(updatedUser: UpdateDepartmentDto, userId: string): Observable<UpdateDepartmentDto> {
    return this.http.put<UpdateDepartmentDto>(`${this.apiUrl}/api/Department/` + userId, updatedUser);
  }

  delete(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/Department/${userId}`);
  }

}