import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InternshipService } from '../../services/internship';
import { Router } from '@angular/router';


@Component({
  selector: 'app-create-internship',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-internship.html'
})
export class CreateInternship {

  internship = {
    title: '',
    description: '',
    requiredSkills: '',
    stipend: 0
  };

  constructor(private internshipService: InternshipService,private router: Router) {}

  createInternship() {

    this.internshipService.createInternship(this.internship).subscribe({
      next: () => {
        alert("Internship created successfully");
        this.router.navigate(['/company-dashboard']);
      },
      error: (err) => {
        console.error(err);
        alert("Error creating internship");
      }
    });

  }

}
