import { Component } from '@angular/core';
import { AdminService } from '../../service/admin';
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './admin-component.html',
  styleUrl: './admin-component.css',
})
export class AdminComponent
{
  admin = {
    email: '',
    password: ''
  }

  showloginForm = false;

  constructor(private adminService: AdminService,
    private router: Router) { }

  login() {
    this.adminService.login(this.admin.email, this.admin.password).subscribe({
      next: (data) => {
        localStorage.setItem("role", "admin");

        alert("Login Successful");
        console.log(data);

        this.router.navigate(['/products']);

      },
      error: (err) => {
        alert("Invalid email or password");
        console.log(err);
      }
    });
  }

  openloginForm() {
    this.showloginForm = true;
  }

  closeloginForm() {
    this.showloginForm = false;
  }
}
