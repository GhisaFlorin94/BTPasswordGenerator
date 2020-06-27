import { PasswordService } from "../services/password.service";
import { Component } from '@angular/core';
import { OneTimePasswordRequest } from "../models/one-time-password-request";
import { Observable } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { timer } from "rxjs/observable/timer";
import { ValidatePasswordRequest } from "../models/validate-password-request";

@Component({
    moduleId: module.id,
    selector: 'password-component',
    templateUrl: 'password.component.html',
    styleUrls: ['password.component.css'],

    providers: [PasswordService]
})
export class PasswordComponent {

    userId: number;
    userIdValid = true;
    validFromDateTime: Date;
    generatedPassword: string;
    
    paswordValidityText:string;
    passwordVality:boolean;

    counter$: Observable<number>;
    count = 60;
constructor(private passwordService: PasswordService) {
    
}
    GetPassword(){
        this.isUserFieldValid();
        if(!this.userIdValid)
            return;
        var passwordRequest: OneTimePasswordRequest ={
            date: new Date(),
            userId: this.userId,
        };
        this.passwordService.getPassword(passwordRequest).subscribe(passwordResponse => {
            this.generatedPassword = passwordResponse.password
            this.validFromDateTime =passwordRequest.date

            this.startCounter(passwordResponse.secondsValidity);
        },
        error => {
            alert("Something went wrong");
            console.error(error)
        }
        );
    }

    ValidatePassword(){


        if(this.generatedPassword == null || this.generatedPassword == ""){
            this.paswordValidityText = "Password is NOT Valid";
            this.passwordVality= false;
            return
        }
        var passwordRequest: ValidatePasswordRequest ={
            password : this.generatedPassword,
            userId: this.userId,
        };
        
        this.passwordService.validatePassword(passwordRequest).subscribe(validityResponse => {
            this.passwordVality=validityResponse;

            if(validityResponse){
                this.paswordValidityText = "Password is Valid"
            }else{
                this.paswordValidityText = "Password is not valid";
            }
        },
        error => {
            alert("Something went wrong");
            console.error(error)
        }
        );
    }

    startCounter(validity: number){
        this.count=validity;
        this.counter$ = timer(0,1000).pipe(
            take(this.count),
            map(() => --this.count)
          );
    }

    isUserFieldValid(){
        if (this.userId != null){
            this.userIdValid = true;
            return;
        }
        this.userIdValid = false;
        return;
    }
}