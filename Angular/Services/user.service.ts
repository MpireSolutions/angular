import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Response, User } from '../../../core/model/index';
import { AppGlobals } from "../../../app.global";

@Injectable({ providedIn: 'root' })
export class UserService {
    private baseurl: string;

    constructor(private http: HttpClient, private appGlobals: AppGlobals) {
        this.baseurl = this.appGlobals.baseApiUrl;       
    }

    getAll() {
        let url = this.baseurl + 'user';
        return this.http.get<Response<User[]>>(url);
    }

    register(user: User) {
        let url = this.baseurl + 'user/create';
        return this.http.post(url, user);
    }
}