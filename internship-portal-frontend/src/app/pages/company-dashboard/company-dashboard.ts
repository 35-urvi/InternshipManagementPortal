import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Company } from '../../services/company';
import { InternshipService } from '../../services/internship';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-company-dashboard',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './company-dashboard.html'
})
export class CompanyDashboard implements OnInit {

  internships: any[] = [];
  applications: any[] = [];
  editingInternship: any = null;

editData = {
  title: '',
  description: '',
  requiredSkills: '',
  stipend: 0
};

  constructor(private companyService: Company,private cd: ChangeDetectorRef,private internshipService: InternshipService,private router: Router) {}

  // ngOnInit() {

  //   const companyId = Number(localStorage.getItem('companyId'));

  //   this.companyService.getMyInternships(companyId).subscribe({
  //     next: (data: any) => {
  //       console.log("Internships:", data);
  //       this.internships = data;
  //     },
  //     error: (err) => console.error(err)
  //   });

  // }
  ngOnInit() {

    if (!localStorage.getItem('token')) {
    this.router.navigate(['/']);
}

  this.companyService.getMyInternships().subscribe({
    next: (data: any) => {

      console.log("Internships:", data);

      this.internships = data;
      this.cd.detectChanges();
      
    },
    error: (err) => console.error(err)
  });

}

  // viewApplications(internshipId: number) {

  //   this.companyService.getApplications(internshipId).subscribe({
  //     next: (data: any) => {
  //       console.log("Applications:", data);
  //       this.applications = data;
  //     },
  //     error: (err) => console.error(err)
  //   });

  // }
  loadingApplications = false;

viewApplications(internshipId: number) {

  this.loadingApplications = true;
  this.applications = [];

  this.companyService.getApplications(internshipId).subscribe({
    next: (data: any) => {

      this.applications = data;

      this.loadingApplications = false;

      this.cd.detectChanges();
    },
    error: (err) => {
      console.error(err);
      this.loadingApplications = false;
    }
  });

}

  accept(id: number) {

    this.companyService.acceptApplication(id).subscribe({
      next: () => {
        alert("Application accepted");
      },
      error: (err) => console.error(err)
    });

  }

  reject(id: number) {

    this.companyService.rejectApplication(id).subscribe({
      next: () => {
        alert("Application rejected");
      },
      error: (err) => console.error(err)
    });

  }

  deleteInternship(id: number) {

  if (!confirm("Are you sure you want to delete this internship?")) {
    return;
  }

  this.internshipService.deleteInternship(id).subscribe({
    next: () => {
      alert("Internship deleted successfully");

      this.internships = this.internships.filter(i => i.id !== id);
      this.ngOnInit()
    },
    error: (err: any) => {
      console.error(err);
      alert("Failed to delete internship");
    }
  });

}

startEdit(internship: any) {

  this.editingInternship = internship;

  this.editData = {
    title: internship.title,
    description: internship.description,
    requiredSkills: internship.requiredSkills,
    stipend: internship.stipend
  };

}

updateInternship() {

  this.internshipService
    .updateInternship(this.editingInternship.id, this.editData)
    .subscribe({
      next: () => {

        alert("Internship updated successfully");

        // update UI instantly
        this.editingInternship.title = this.editData.title;
        this.editingInternship.description = this.editData.description;
        this.editingInternship.requiredSkills = this.editData.requiredSkills;
        this.editingInternship.stipend = this.editData.stipend;

        this.editingInternship = null;
        this.ngOnInit()
      },

      error: (err: any) => {
        console.error(err);
        alert("Update failed");
      }
    });

}

goToCreateInternship() {
  this.router.navigate(['/create-internship']);
}

logout() {

  localStorage.removeItem('token');

  this.router.navigate(['/']);

}





}
