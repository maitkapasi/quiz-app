import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
 
@Injectable()
export class ApiHandlerService {

    constructor(private http:HttpClient) {}

    get<T>(url: string, params: any = {}) : Observable<T> {
        return this.http.get<T>(url, {params: params});
    }

    post<B,R>(uri: string, body: B): Observable<R> {
        return this.http.post<R>(uri, body);
    }

    postNoBody<R>(uri: string): Observable<R> {
        return this.http.post<R>(uri, null);
    }
}