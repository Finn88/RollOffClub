import { Component, output } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrl: './register-user.component.css',
  imports: [RouterLink]
})
export class RegisterUserComponent {

  create() {
  }
}
