import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-register-student',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register-student.html'
})
export class RegisterStudent {

  student = {
    name: '',
    email: '',
    password: '',
    department: '',
    cgpa: 0
  };

  constructor(private auth: Auth) {}

  register() {

    this.auth.registerStudent(this.student).subscribe({
      next: (res) => {
        alert("Student registered successfully");
      },
      error: (err) => {
        console.error(err);
        alert("Registration failed");
      }
    });

  }

}
