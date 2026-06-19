import { Component } from '@angular/core';
import { User } from '../user/user';
import { AdminComponent } from '../admin-component/admin-component';

@Component({
  selector: 'app-home-component',
  standalone: true,
  imports: [User, AdminComponent],
  templateUrl: './home-component.html',
  styleUrls: ['./home-component.css']
})
export class HomeComponent { }
