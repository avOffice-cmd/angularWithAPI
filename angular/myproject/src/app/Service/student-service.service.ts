import { Injectable } from '@angular/core';
import { showStudent } from '../models/Students';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class StudentServiceService {

  studentsList: showStudent[] = [];

  constructor(private http: HttpClient) {
  }

  // public getAllStudents(): Student[] {
  //   let student1 = new Student();
  //   student1.id = 1;
  //   student1.StudentFullName = "Achal Verma";
  //   student1.StudentAge = 22;
  //   student1.StudentAddress = "abc";
  //   student1.StudentEmail = "achal@gmail.com";

  //   let student2 = new Student();
  //   student2.id = 2;
  //   student2.StudentFullName = "Another Student";
  //   student2.StudentAge = 25;
  //   student2.StudentAddress = "xyz";
  //   student2.StudentEmail = "another@gmail.com";

  //   // Use 'this.students' to refer to the member variable
  //   this.studentsList.push(student1, student2);

  //   return this.studentsList;
  // }



  // get student data

  fetchData() {

    this.http.get('https://localhost:7246/api/Student/getall').subscribe((response: any) => {
      // Handle the response data here

      let gotStudentDtoList: showStudent[] = response;
      this.studentsList = gotStudentDtoList;
    });
  }


  addStudent(_studentFullName: string, _studentAge: string, _studentEmail: string, _studentAddress: string, _studentCityOrCountry: string) {
    var newStudent = {
      'studentFullName': _studentFullName,
      'studentAge': _studentAge,
      'studentEmail': _studentEmail,
      'studentAddress': _studentAddress,
      'studentCityOrCountry': _studentCityOrCountry
    }

    this.http.post('https://localhost:7246/api/Student/add', newStudent).subscribe((response: any) => {
      console.log(response);
    });

  }


  updateStudent(
    _studentIDinp: number, _studentNameinp: string, 
    _studentAgeinp: string, _studentEmailinp: string,
    _studentAddress:string,  _studentGroupCount:string,
     _studentCityinp: string,_studentCountryinp:string
  ) : any
   {
    var updateStudent = {
      "studentID": _studentIDinp,
      "studentAge": _studentAgeinp,
      "studentAddress": _studentAddress,
      "studentCity": _studentCityinp,
      "studentName": _studentNameinp,
      "studentEmail": _studentEmailinp,
      "ageGroupCount": _studentGroupCount,
      "studentCountry" : _studentCountryinp
    }
   return this.http.put('https://localhost:7246/api/Student/update', updateStudent).subscribe((response: any) => {
      console.log(response);
    });
  }

  
}
