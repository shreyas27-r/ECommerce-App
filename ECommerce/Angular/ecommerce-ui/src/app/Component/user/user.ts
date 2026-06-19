import { Component } from '@angular/core';
import { UserService } from '../../service/user';
import { FormsModule } from '@angular/forms';
import { CommonModule } from "@angular/common";
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  imports: [FormsModule, CommonModule],
  templateUrl: './user.html',
  styleUrl: './user.css',
})
export class User {
    Reguser = {
    id: 0,
    name: '',
    email: '',
    password: ''
    };

  LgUser = {
    id:0,
    email: '',
    password: ''
  };

  showRegisterForm = false;
  showloginForm = false;


     users : any[] = [];
  constructor(private userService: UserService,
    private router: Router  ) { }

  createUser() {
    console.log("Reguser:", this.Reguser);

    this.userService.createUser(this.Reguser).subscribe({
      next: () => {
        alert("Register Successful");
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getUsers() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        console.log(data);
        this.users = data;
      }
    });
  }

  login() {
    this.userService.login(this.LgUser.email, this.LgUser.password).subscribe({
      next: (data) => {
        localStorage.setItem("role", "user");
        localStorage.setItem("userId", data.userId.toString());
        localStorage.setItem("userName", data.userName);
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

  openRegisterForm() {
    this.showRegisterForm = true;
  }

  closeRegisterForm() {
    this.showRegisterForm = false;
  }

  openloginForm() {
    this.showloginForm = true;
  }

  closeloginForm() {
    this.showloginForm = false;
  }
}




