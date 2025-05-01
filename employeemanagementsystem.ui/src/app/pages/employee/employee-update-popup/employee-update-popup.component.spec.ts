import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeUpdatePopupComponent } from './employee-update-popup.component';

describe('EmployeeUpdatePopupComponent', () => {
  let component: EmployeeUpdatePopupComponent;
  let fixture: ComponentFixture<EmployeeUpdatePopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeeUpdatePopupComponent]
    });
    fixture = TestBed.createComponent(EmployeeUpdatePopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
