import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService, AlertService } from "src/app/core/services/index";
import { DashboardComponent } from '../../../dashboard/components/dashboard/dashboard.component';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    passwordType: string = "password";
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', [Validators.required,Validators.maxLength(30)]],
            password: ['', [Validators.required,Validators.maxLength(20)]]
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/CreateLead/0/LeadInformation';
    }

    // convenience getter for easy access to form fields
    get loginFormControls() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login(this.loginFormControls.username.value, this.loginFormControls.password.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.loading = false;

                    this.authenticationService.currentUser
                    this.router.navigate([this.returnUrl]);
                },
                error => {
                    if (error.status == 0) {
                        this.alertService.displayError("", "Api under development!", 3000);

                    } else {
                        if(error.error.errorDetails){
                            this.alertService.displayError("", error.error.errorDetails[0].errorMessage, 3000);
                        }
                        else{
                            this.alertService.displayError("", "Error - Login Failed", 3000);                        
                        }
                    }                    

                    if(error.error.errorDetails){
                        this.alertService.error(error.error.errorDetails[0].errorMessage);
                    }
                    else{
                        this.alertService.error("Error - Login Failed");                        
                    }
                    this.loading = false;
                }
            );
    }

    changePasswordType() {
        if (this.passwordType == "password") {
            this.passwordType = "text";
        }else{
            this.passwordType = "password";
        }
    }
}