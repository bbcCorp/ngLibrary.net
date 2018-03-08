import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class LibraryService {

  constructor(@Inject('apiUrl') private apiUrl, private http: HttpClient) { }

  getBooks() {
    console.log('Getting book list from: '+ this.apiUrl + '/books' );
    return this.http
        .get<string[]>(this.apiUrl + 'books');
}

}
