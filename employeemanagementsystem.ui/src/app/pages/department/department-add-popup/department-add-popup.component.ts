import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DepartmentDto } from 'src/app/models/department/departmentDto';
import { AddDepartmentDto } from 'src/app/models/department/addDepartmentDto';

@Component({
  selector: 'app-department-add-popup',
  templateUrl: './department-add-popup.component.html',
  styleUrls: ['./department-add-popup.component.css']
})
export class DepartmentAddPopupComponent {
  myForm!: FormGroup;
  departments: DepartmentDto[] = [];
  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DepartmentAddPopupComponent>,
  ) {}

  data: AddDepartmentDto = {
    departmentName: '',
  };

  initializeForm() {
    this.myForm = this.fb.group({
      departmentName: [
        '',
        [Validators.required, Validators.maxLength(50)],
      ],
    });
  }

  ngOnInit() {
    this.initializeForm();
  }

  saveAll() {
    this.dialogRef.close(this.myForm.value);
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}

