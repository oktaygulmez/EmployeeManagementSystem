import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeDto } from 'src/app/models/employee/employeeDto';
import { AddEmployeeDto } from 'src/app/models/employee/addEmployeeDto';
import { UpdateEmployeeDto } from 'src/app/models/employee/updateEmployeeDto';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  userLogin(userName: string, password: string): Observable<{ token: string }> {

    const body = {
      eMail: userName,
      password: password
    };

    return this.http.post<{ token: string }>(`${this.apiUrl}/api/Auth/login`, body);
  }

  getall(): Observable<EmployeeDto[]> {
    return this.http.get<EmployeeDto[]>(`${this.apiUrl}/api/Employee/`);
  }

  add(newUser: AddEmployeeDto): Observable<AddEmployeeDto> {
    return this.http.post<AddEmployeeDto>(`${this.apiUrl}/api/Employee/`, newUser);
  }

  update(updatedUser: UpdateEmployeeDto, userId: string): Observable<UpdateEmployeeDto> {
    console.log(updatedUser);
    return this.http.put<UpdateEmployeeDto>(`${this.apiUrl}/api/Employee/` + userId, updatedUser);
  }

  delete(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/Employee/${userId}`);
  }

}
