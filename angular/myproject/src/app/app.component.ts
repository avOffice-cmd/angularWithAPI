import { Component } from '@angular/core';
import { StudentServiceService } from './Service/student-service.service';
import { showStudent } from '../app/models/Students';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'myProject';

  students: showStudent[] = [];

  constructor(private StudentServiceService:StudentServiceService){
    this.show()
  }


// GET  STUDENT //
  show()
  {

    this.StudentServiceService.fetchData();
    this.students = this.StudentServiceService.studentsList;
    // console.log(this.students[0]);
    
  }


// ADD STUDENT //
  addStudent(_nameInp:string,
             _ageInp:string,
             _emailInp:string,
             _addressInp:string,
             _cityOrCountryInp:string)
  {
        this.StudentServiceService.addStudent(_nameInp,_ageInp,_emailInp,_addressInp,_cityOrCountryInp);

  }

  
}
