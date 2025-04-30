import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAddPopupComponent } from './employee-add-popup.component';

describe('EmployeeAddPopupComponent', () => {
  let component: EmployeeAddPopupComponent;
  let fixture: ComponentFixture<EmployeeAddPopupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeeAddPopupComponent]
    });
    fixture = TestBed.createComponent(EmployeeAddPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
