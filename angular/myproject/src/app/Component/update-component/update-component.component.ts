import { Component } from '@angular/core';
import { StudentServiceService } from 'src/app/Service/student-service.service';

@Component({
  selector: 'app-update-component',
  templateUrl: './update-component.component.html',
  styleUrls: ['./update-component.component.css']
})
export class UpdateComponentComponent {

  constructor(private StudentServiceService:StudentServiceService){}

  updateStudent(_studentIDinp: number, _studentNameinp: string, 
    _studentAgeinp: string, _studentEmailinp: string,
    _studentAddress:string,  _studentGroupCount:string,
     _studentCityinp: string, _studentCountryinp:string) 
     {

 const res =     this.StudentServiceService.updateStudent(_studentIDinp, _studentNameinp,
                                             _studentAgeinp, _studentEmailinp,
                                             _studentAddress, _studentGroupCount,
                                             _studentCityinp, _studentCountryinp);
                  if(res.status == 200){
                    this.StudentServiceService.fetchData();
                  }

                    

  }
}
