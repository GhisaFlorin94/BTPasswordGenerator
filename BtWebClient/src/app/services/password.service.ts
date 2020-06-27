import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OneTimePasswordResponse } from '../models/one-time-password-response';
import { OneTimePasswordRequest } from '../models/one-time-password-request';
import { AppPaths } from '../AppPaths';
import { ValidatePasswordRequest } from '../models/validate-password-request';

@Injectable()
export class PasswordService {

    headers: HttpHeaders;

    constructor(private httpClient: HttpClient) {
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });
        this.headers.append('Content-Type', 'application/json');
    }

    getPassword(passwordRequest: OneTimePasswordRequest): Observable<OneTimePasswordResponse> {
        return this.httpClient.post(AppPaths.GetPassword,
            passwordRequest,
            {
                headers: this.headers
            }).pipe(catchError(this.handleError));
    }

    validatePassword(validateRequest: ValidatePasswordRequest): Observable<boolean> {
        return this.httpClient.post(AppPaths.ValidatePassword,
            validateRequest,
            {
                headers: this.headers
            }).pipe(catchError(this.handleError));
    }

    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
}