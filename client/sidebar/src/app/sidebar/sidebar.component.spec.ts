import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SidebarComponent } from './sidebar.component';
import { By } from '@angular/platform-browser';

describe('SidebarComponent', () => {
  let component: SidebarComponent;
  let fixture: ComponentFixture<SidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SidebarComponent], // SidebarComponent is standalone
    }).compileComponents();

    fixture = TestBed.createComponent(SidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle isOpen state when toggleSidebar is called', () => {
    expect(component.isOpen).toBeFalse();
    component.toggleSidebar();
    expect(component.isOpen).toBeTrue();
    component.toggleSidebar();
    expect(component.isOpen).toBeFalse();
  });

  it('should add "open" class when isOpen is true', () => {
    const sidebarElement = fixture.debugElement.query(By.css('.sidebar'));

    expect(sidebarElement.nativeElement.classList).not.toContain('open');

    component.toggleSidebar();
    fixture.detectChanges();

    expect(sidebarElement.nativeElement.classList).toContain('open');
  });

  it('should call toggleSidebar when the button is clicked', () => {
    spyOn(component, 'toggleSidebar');

    const button = fixture.debugElement.query(By.css('.open-btn'));
    button.triggerEventHandler('click', null);

    expect(component.toggleSidebar).toHaveBeenCalled();
  });
});
