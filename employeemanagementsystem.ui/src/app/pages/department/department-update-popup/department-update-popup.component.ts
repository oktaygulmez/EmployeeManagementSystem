import { Component, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DepartmentDto } from 'src/app/models/department/departmentDto';
import { UpdateDepartmentDto } from 'src/app/models/department/updateDepartmentDto';

@Component({
  selector: 'app-department-update-popup',
  templateUrl: './department-update-popup.component.html',
  styleUrls: ['./department-update-popup.component.css']
})
export class DepartmentUpdatePopupComponent {
  myForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DepartmentUpdatePopupComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { department: DepartmentDto }
  ) {}

  user: UpdateDepartmentDto = {
    id:'',
    departmentName: '',
  };

  initializeForm() {
    this.myForm = this.fb.group({
      id:this.data.department.id,
      departmentName: [
        this.data.department.departmentName,
        [Validators.required, Validators.maxLength(50)],
      ],
    });
  }

  ngOnInit() {
    console.log(this.data.department)
    this.initializeForm();
  }

  saveAll() {
    this.dialogRef.close(this.myForm.value);
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}

