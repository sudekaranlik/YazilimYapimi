import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators,FormBuilder } from "@angular/forms";
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;
  constructor(private formBuilder:FormBuilder, private authService:AuthService, private toastrService:ToastrService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm=this.formBuilder.group({
      firstName:["",Validators.required],
      lastName:["",Validators.required],
      email:["",Validators.required],
      password:["",Validators.required],
      userName:["",Validators.required],
      address:["",Validators.required],
      telefonNo:["",Validators.required],
      tcKimlik:["",Validators.required]
    })
  }

  register(){
    if(this.registerForm.valid){
      console.log(this.registerForm.value);
      let loginModel= Object.assign({},this.registerForm.value)

      this.authService.login(loginModel).subscribe(response=>{
        this.toastrService.info(response.message)
        localStorage.setItem("token",response.data.token)
        console.log(response.data.token)
      },responseError=>{
        //console.log(responseError)
        this.toastrService.error(responseError.error)
      })
    }
  }
}
