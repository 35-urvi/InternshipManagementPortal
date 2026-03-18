import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Student } from '../../services/student';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './student-dashboard.html'
})
export class StudentDashboard implements OnInit {

  applications: any[] = [];
  recommendedInternships: any[] = [];

  constructor(
    private studentService: Student,
    private cd: ChangeDetectorRef,
    private router: Router
  ) {}

ngOnInit() {

  this.studentService.getMyApplications().subscribe({
    next: (data: any) => {

      console.log("Applications:", data);

      this.applications = data;

      this.cd.detectChanges();
    },
    error: (err) => {
      console.error(err);
    }
  });

  this.studentService.getRecommendedInternships().subscribe({
    next: (data: any) => {
      console.log("Recommended:", data);
      this.recommendedInternships = data;
      this.cd.detectChanges();
    },
    error: (err) => {
      console.error(err);
    }
  });

}

viewInternships() {
  this.router.navigate(['/internships']);
}

logout() {

  localStorage.removeItem('token');

  this.router.navigate(['/']);

}



}
