import { Component, Inject, Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class FilesService {
    public _files: BehaviorSubject<Group[]>;
    private apiUrl;

    private dataStore: {
        files: Group[]
    };

    constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.dataStore = { files: [] };
        this._files = <BehaviorSubject<Group[]>>new BehaviorSubject([]);
        this.apiUrl = baseUrl;
    }

    get files() {
        return this._files.asObservable();
    }

    retrieveFilesData() {
        return this.http.get<Group[]>(this.apiUrl + 'api/file/get-files/').subscribe(result => {
            this.dataStore.files = result as Group[];
            this._files.next(Object.assign({}, this.dataStore).files);
        }, error => console.error(error));
    }
}

export interface Group {
    Extension: string,
    Files: File[]
}

export interface File {
    Id: string;
    FileName: string;
    UserId: string;
    Extension: string;
    User: User;
}

export interface User {
    Email: string;
}
