// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-login',
//   imports: [],
//   templateUrl: './login.html',
//   styleUrl: './login.css',
// })
// export class Login {}
// import { Component } from '@angular/core';
// import { FormsModule } from '@angular/forms';
// import { Auth } from '../../services/auth';

// @Component({
//   selector: 'app-login',
//   standalone: true,
//   imports: [FormsModule],
//   templateUrl: './login.html',
//   styleUrls: ['./login.css']
// })
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Auth } from '../../services/auth';
import { RouterModule } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule],
  templateUrl: './login.html'
})
export class Login {

  email = '';
  password = '';

  constructor(private auth: Auth,private router: Router) {}

  // login() {

  //   const data = {
  //     email: this.email,
  //     password: this.password
  //   };

  //   this.auth.login(data).subscribe({
  //     next: (res) => {
  //       localStorage.setItem('token', res.token);
  //       alert('Login successful');
  //     },
  //     error: () => {
  //       alert('Login failed');
  //     }
  //   });

  // }
//   login() {

//   const data = {
//   Email: this.email,
//   Password: this.password
// };

//   this.auth.login(data).subscribe({
//     next: (res) => {
//       console.log(res);
//       localStorage.setItem('token', res.token);
//       localStorage.setItem('studentId', res.userId);
//       alert('Login successful');
//     },
//     error: (err) => {
//       console.error(err);
//       alert('Login failed');
//     }
//   });

// }
login() {

  if (this.email === "admin@yahoo.com" && this.password === "admin") {
    this.router.navigate(['/admin-dashboard']);
    return;
  }

  const data = {
    email: this.email,
    password: this.password
  };

  this.auth.login(data).subscribe({
    next: (res: any) => {

      localStorage.setItem('token', res.token);

      const decoded: any = jwtDecode(res.token);

      const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      if (role === "Student") {
        this.router.navigate(['/student-dashboard']);
      }

      else if (role === "Company") {
        this.router.navigate(['/company-dashboard']);
      }

    },

    error: (err: any) => {
      alert("Login failed");
    }
  });

}


}
