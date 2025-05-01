import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { DeletepopupComponent } from '../../component/deletepopup/deletepopup.component';
import { EmployeeService } from 'src/app/services/employee.service';
import { DepartmentService } from 'src/app/services/department.service';
import { DepartmentDto } from 'src/app/models/department/departmentDto';
import { DepartmentAddPopupComponent } from './department-add-popup/department-add-popup.component';
import { DepartmentUpdatePopupComponent } from './department-update-popup/department-update-popup.component';
import { UpdateDepartmentDto } from 'src/app/models/department/updateDepartmentDto';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css'],
})
export class DepartmentComponent implements OnInit {
  displayedColumns: string[] = ['departmentName', 'actions'];
  dataSource = new MatTableDataSource<DepartmentDto>();
  departments: DepartmentDto[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private toastr: ToastrService,
    public dialog: MatDialog,
    private departmentService: DepartmentService,
    private employeeService: EmployeeService
  ) {}

  ngOnInit() {
    this.getDeparmentList();
  }

  getDeparmentList() {
    this.departmentService.getall().subscribe((result: DepartmentDto[]) => {
      this.dataSource = new MatTableDataSource<DepartmentDto>(result);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  searchText: string = '';

  applyFilter(): void {
    this.dataSource.filter = this.searchText.trim().toLowerCase();
  }

  openAddDialog() {
    const updatePopup = this.dialog.open(DepartmentAddPopupComponent, {
      width: '600px',
      data: { departments: this.departments },
    });
    updatePopup.afterClosed().subscribe((add) => {
      if (add) {
        this.departmentService.add(add).subscribe(
          (result) => {
            this.toastr.success('Departman Eklendi', 'Başarılı');
            this.getDeparmentList();
          },
          (error) => {
            this.toastr.error('Departman Eklenemedi', 'HATA');
          }
        );
      }
    });
  }

  openEditDialog(department: UpdateDepartmentDto) {
    const updatePopup = this.dialog.open(DepartmentUpdatePopupComponent, {
      width: '600px',
      data: { department: department },
    });
    updatePopup.afterClosed().subscribe((updated) => {
      if (updated) {
        this.departmentService.update(updated, department.id).subscribe(
          (result) => {
            this.toastr.success('Departman Güncellendi', 'Başarılı');
            this.getDeparmentList();
          },
          (error) => {
            this.toastr.error('Departman Güncellenemedi', 'HATA');
          }
        );
      }
    });
  }

  deleteEmployee(id: any) {
    this.employeeService.getall().subscribe((result) => {
      const filtered = result.filter((x) => x.departmentId == id);
      if (filtered.length > 0) {
        this.toastr.error(
          'Bu departmanda ' +
            filtered.length +
            ' kayıt var. Silemezsiniz. Silmek için departmandaki tüm çalışanları silmeniz gerekmektedir.',
          'HATA'
        );
      } else {
        const deletePopup = this.dialog.open(DeletepopupComponent);

        deletePopup.afterClosed().subscribe((popupResult) => {
          if (popupResult) {
            this.departmentService.delete(id).subscribe(
              () => {
                this.toastr.success('Departman Silindi', 'Başarılı');
                this.getDeparmentList();
              },
              () => {
                this.toastr.error('Departman Silinemedi', 'HATA');
              }
            );
          }
        });
      }
    });
  }
}
