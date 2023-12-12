import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentComponentComponent } from './Component/student-component/student-component.component';
import { UpdateComponentComponent } from './Component/update-component/update-component.component';

const routes: Routes = [
  {
    path:'student',
    component: StudentComponentComponent
  },
  {
    path:'update',
    component: UpdateComponentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
