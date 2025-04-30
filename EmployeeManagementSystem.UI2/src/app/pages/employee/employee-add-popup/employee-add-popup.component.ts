import { Component, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddEmployeeDto } from 'src/app/models/employee/addEmployeeDto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { DepartmentDto } from 'src/app/models/department/departmentDto';

@Component({
  selector: 'app-employee-add-popup',
  templateUrl: './employee-add-popup.component.html',
  styleUrls: ['./employee-add-popup.component.css']
})
export class EmployeeAddPopupComponent {
  myForm!: FormGroup;
  departments: DepartmentDto[] = [];
  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<EmployeeAddPopupComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { departments: DepartmentDto[] }
  ) {}

  user: AddEmployeeDto = {
    name: '',
    surName: '',
    eMail: '',
    phone: '',
    adress: '',
    departmentId: '',
  };

  initializeForm() {
    this.myForm = this.fb.group({
      name: [
        '',
        [Validators.required, Validators.maxLength(50)],
      ],
      surName: [
        '',
        [Validators.required, Validators.maxLength(50)],
      ],
      eMail: [
        '',
        [Validators.required, Validators.email, Validators.maxLength(100)],
      ],
      phone: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[0-9]{10,15}$/), // 10-15 haneli sadece rakam
          Validators.maxLength(15),
        ],
      ],
      adress: [
        '',
        [Validators.required, Validators.maxLength(250)],
      ],
      departmentId: ['', [Validators.required]],
    });
  }

  ngOnInit() {
    this.departments = this.data.departments;
    this.initializeForm();
  }

  saveUser() {
    this.dialogRef.close(this.myForm.value);
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}
