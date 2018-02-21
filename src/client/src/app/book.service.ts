import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { map } from 'rxjs/operators';
import 'rxjs/add/operator/map';

@Injectable()
export class BookService {

    constructor(private http: HttpClient) {

    }

    getBooks() {
        const apiUrl = 'http://localhost:5000/api/books';

        // Note: The map function used here is the Rxjs map function
        // which works on Observables
        return this.http
            .get<string[]>(apiUrl);
    }
}

