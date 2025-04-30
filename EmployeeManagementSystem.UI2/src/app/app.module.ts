import { HttpClientModule } from '@angular/common/http';
import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';
import { FormsModule } from '@angular/forms'; 
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe, NgIf } from '@angular/common';
import { NgFor } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { ToastrModule } from 'ngx-toastr';
import { FooterComponent } from './component/footer/footer.component';
import { HeaderComponent } from './component/header/header.component';
import { SidebarComponent } from './component/sidebar/sidebar.component';
import { DeletepopupComponent } from './component/deletepopup/deletepopup.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';

//sayfalar
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { UnAuthorizedComponent } from './pages/un-authorized/un-authorized.component';
import { EmployeeComponent } from './pages/employee/employee.component';
import { DepartmentComponent } from './pages/department/department.component';
import { DepartmentAddPopupComponent } from './pages/department/department-add-popup/department-add-popup.component';
import { DepartmentUpdatePopupComponent } from './pages/department/department-update-popup/department-update-popup.component';
import { EmployeeAddPopupComponent } from './pages/employee/employee-add-popup/employee-add-popup.component';
import { EmployeeUpdatePopupComponent } from './pages/employee/employee-update-popup/employee-update-popup.component';

registerLocaleData(localeTr);

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    DeletepopupComponent,

    //sayfalarım
    LoginComponent,
    NotFoundComponent,
    UnAuthorizedComponent,
    EmployeeComponent,
    DepartmentComponent,
    DepartmentAddPopupComponent,
    DepartmentUpdatePopupComponent,
    EmployeeAddPopupComponent,
    EmployeeUpdatePopupComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    //Popup
    MatDialogModule,

    //Toastr
    ToastrModule.forRoot({
      timeOut: 9000, // Bildirimin görüntülenme süresi (ms)
      positionClass: 'toast-bottom-right', // Bildirimin görüntüleneceği konum
      //  preventDuplicates: true, // Aynı bildirimin tekrar gösterilmemesini sağlar
    }),

    //form parçaları
    FormsModule,
    MatIconModule,
    MatButtonModule,
    NgIf,
    NgFor,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatListModule,
    MatSelectModule,
    MatCheckboxModule,

    //Mat table parçaları
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  providers: [
    //{
    //  provide: MAT_DATE_LOCALE, //dateInput türkçe ayarı
    //  useValue: 'tr-TR'
    //},
    { provide: LOCALE_ID, useValue: 'tr-TR' },
    { provide: MAT_DATE_LOCALE, useValue: 'tr-TR' },
    DatePipe,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
