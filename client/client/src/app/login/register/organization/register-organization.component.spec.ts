import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterOrganizationComponent } from './register-organization.component';
import { RegisterService } from '../../../_services/register/register.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Organization } from '../../../_models/organization';

describe('OrganizationComponent', () => {
  let component: RegisterOrganizationComponent;
  let fixture: ComponentFixture<RegisterOrganizationComponent>;
  let registerService: RegisterService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports : [HttpClientModule, RouterTestingModule, RegisterOrganizationComponent],
      providers:[RegisterService]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterOrganizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    registerService = TestBed.inject(RegisterService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should register', () => {   
    const orgData:Organization = {   
      name: "A",
      description: "B",
      country: "C",
      city: "D"
     };
     component.registerForm.get("name")?.setValue("A");
     component.registerForm.get("description")?.setValue("B");
     component.registerForm.get("country")?.setValue("C");
     component.registerForm.get("city")?.setValue("D");

    spyOn(registerService, 'register').and.callThrough();
    component.submit(); 
    expect(registerService.register).toHaveBeenCalledWith(orgData); 
  });

});
