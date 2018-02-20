import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
// import { map } from 'rxjs/operators';
import 'rxjs/add/operator/map'

@Injectable()
export class BookService{

    constructor(private http: Http){

    }

    getBooks(){
        let apiUrl = "http://localhost:5000/api/books";

        // Note: The map function used here is the Rxjs map function
        // which works on Observables
        return this.http
            .get(apiUrl)
            .map((res: Response) => {
                return res.json()
            });
    }
}

