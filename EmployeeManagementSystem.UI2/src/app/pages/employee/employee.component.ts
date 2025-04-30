import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeDto } from 'src/app/models/employee/employeeDto';
import { AddEmployeeDto } from 'src/app/models/employee/addEmployeeDto';
import { DeletepopupComponent } from '../../component/deletepopup/deletepopup.component';
import { EmployeeUpdatePopupComponent } from './employee-update-popup/employee-update-popup.component';
import { DepartmentService } from 'src/app/services/department.service';
import { DepartmentDto } from 'src/app/models/department/departmentDto';
import { EmployeeAddPopupComponent } from './employee-add-popup/employee-add-popup.component';
import { UpdateEmployeeDto } from 'src/app/models/employee/updateEmployeeDto';
@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  displayedColumns: string[] = [
    'name',
    'surName',
    'eMail',
    'phone',
    'adress',
    'departmentName',
    'actions',
  ];
  dataSource = new MatTableDataSource<EmployeeDto>();
  employees: EmployeeDto[] = [];
  departments: DepartmentDto[] = [];

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  constructor(
    private toastr: ToastrService,
    public dialog: MatDialog,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService
  ) {}

  ngOnInit() {
    this.getEmployeeList();
    this.getDepartmentList();
  }

  getDepartmentList() {
    this.departmentService.getall().subscribe((result: DepartmentDto[]) => {
      this.departments = result;
    });
  }

  getEmployeeList() {
    this.employeeService.getall().subscribe((result: EmployeeDto[]) => {
      this.dataSource = new MatTableDataSource<EmployeeDto>(result);
      this.employees = result;
      this.dataSource.paginator = this.paginator;
    });
  }

  selectedDepartment: string = '';
  searchText: string = '';

  applyFilter() {
    this.dataSource.data = this.employees
      .filter((x) =>  !this.selectedDepartment || x.departmentId === this.selectedDepartment)
      .filter(
        (x) =>
          x.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
          x.surName.toLowerCase().includes(this.searchText.toLowerCase()) ||
          x.eMail.toLowerCase().includes(this.searchText.toLowerCase()) ||
          x.phone.toLowerCase().includes(this.searchText.toLowerCase()) ||
          x.adress.toLowerCase().includes(this.searchText.toLowerCase())
      );
  }

  openAddDialog() {
    const updatePopup = this.dialog.open(EmployeeAddPopupComponent, {
      width: '600px',
      data: { departments: this.departments },
    });
    updatePopup.afterClosed().subscribe((updatedUser) => {
      if (updatedUser) {
        this.employeeService.add(updatedUser).subscribe( result => {
            this.toastr.success('Çalışan Eklendi', 'Başarılı');
            this.getEmployeeList();
          },
          (error) => {
            this.toastr.error('Çalışan Eklenemedi', 'HATA');
          }
        );
      }
    });
  }

  openEditDialog(user: UpdateEmployeeDto) {
    const updatePopup = this.dialog.open(EmployeeUpdatePopupComponent, {
      width: '600px',
      data: { user: user, departments: this.departments },
    });
    updatePopup.afterClosed().subscribe((updatedUser) => {
      if (updatedUser) {
        this.employeeService.update(updatedUser, user.id).subscribe( result => {
            this.toastr.success('Çalışan Güncellendi', 'Başarılı');
            this.getEmployeeList();
          },
          (error) => {
            this.toastr.error('Çalışan Güncellenemedi', 'HATA');
          }
        );
      }
    });
  }

  deleteEmployee(userId: any) {
    const deletePopup = this.dialog.open(DeletepopupComponent);
    deletePopup.afterClosed().subscribe((result) => {
      if (result) {
        this.employeeService.delete(userId).subscribe( result => {
            this.toastr.success('Çalışan Silindi', 'Başarılı');
            this.getEmployeeList();
          },
          (error) => {
            this.toastr.error('Çalışan Silinemedi', 'HATA');
          }
        );
      }
    });
  }
}
