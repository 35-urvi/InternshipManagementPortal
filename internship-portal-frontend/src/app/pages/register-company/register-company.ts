import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-register-company',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register-company.html'
})
export class RegisterCompany {

  company = {
    companyName: '',
    industry: '',
    email: '',
    password: ''
  };

  constructor(private auth: Auth) {}

  register() {

    this.auth.registerCompany(this.company).subscribe({
      next: () => {
        alert("Company registered successfully");
      },
      error: (err) => {
        console.error(err);
        alert("Registration failed");
      }
    });

  }

}
