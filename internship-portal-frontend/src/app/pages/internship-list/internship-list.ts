import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InternshipService } from '../../services/internship';
import { Application } from '../../services/application';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';


@Component({
  selector: 'app-internship-list',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './internship-list.html'
})
export class InternshipList implements OnInit {

  internships: any[] = [];

  keyword = '';
  skill = '';
  minStipend?: number;

  constructor(
    private internshipService: InternshipService,
    private applicationService: Application,
    private cd: ChangeDetectorRef,
    private router:Router
  ) {}

  ngOnInit() {

    console.log("Component Loaded");

    this.internshipService.getAllInternships().subscribe({
      next: (data: any) => {

        console.log("API Response:", data);

        this.internships = data;
        console.log(this.internships);
        // FORCE Angular to refresh UI
        this.cd.detectChanges();
      },
      error: (err) => {
        console.error(err);
      }
    });

  }

  search() {

  const stipend = this.minStipend ? Number(this.minStipend) : undefined;

  this.internshipService.searchInternships(
    this.keyword,
    this.skill,
    stipend
  ).subscribe({
    next: (data: any) => {
      this.internships = data;
      this.cd.detectChanges();
    }
  });

  }

  apply(internshipId: number) {

  this.applicationService.apply(internshipId).subscribe({
    next: (res) => {
      alert("Application submitted successfully");
      console.log(res);
    },
    error: (err) => {
      console.error(err);
      alert(err.error);
    }
  });

}

goBack() {

  const token = localStorage.getItem('token');

  if (!token) {
    this.router.navigate(['/admin-dashboard']);
    return;
  }

  const decoded: any = jwtDecode(token);

  const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  if (role === "Student") {
    this.router.navigate(['/student-dashboard']);
  }

  else if (role === "Company") {
    this.router.navigate(['/company-dashboard']);
  }

  else if (role === "Admin") {
    this.router.navigate(['/admin-dashboard']);
  }

}



  

}
