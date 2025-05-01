import { Component, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeDto } from 'src/app/models/employee/employeeDto';
import { UpdateEmployeeDto } from 'src/app/models/employee/updateEmployeeDto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; // Import Validators and FormBuilder
import { DepartmentDto } from 'src/app/models/department/departmentDto';

@Component({
  selector: 'app-employee-update-popup',
  templateUrl: './employee-update-popup.component.html',
  styleUrls: ['./employee-update-popup.component.css']
})
export class EmployeeUpdatePopupComponent {
  myForm!: FormGroup;
  departments: DepartmentDto[] = [];
  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<EmployeeUpdatePopupComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { user: EmployeeDto; departments: DepartmentDto[] }
  ) {}

  user: UpdateEmployeeDto = {
    id: '',
    name: '',
    surName: '',
    eMail: '',
    phone: '',
    adress: '',
    departmentId: '',
  };

  initializeForm() {
    this.myForm = this.fb.group({
      id: this.data.user.id,
      name: [
        this.data.user.name,
        [Validators.required, Validators.maxLength(50)],
      ],
      surName: [
        this.data.user.surName,
        [Validators.required, Validators.maxLength(50)],
      ],
      eMail: [
        this.data.user.eMail,
        [Validators.required, Validators.email, Validators.maxLength(100)],
      ],
      phone: [
        this.data.user.phone,
        [
          Validators.required,
          Validators.pattern(/^[0-9]{10,15}$/), // 10-15 haneli sadece rakam
          Validators.maxLength(15),
        ],
      ],
      adress: [
        this.data.user.adress,
        [Validators.required, Validators.maxLength(250)],
      ],
      departmentId: [this.data.user.departmentId, [Validators.required]],
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
