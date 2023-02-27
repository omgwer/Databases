import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Route} from "../container/route.interface";
import {Restrictions} from "../container/restrictions.interface";

export interface IOptionResponse {
  message: string;
  model?: Route[];
}

@Injectable()
export class RouteHelper {
  private baseUrl: string = 'https://localhost:7181/';

  constructor(private http: HttpClient) {
    this.http = http;
  }

  getRouteList(index: Number): Observable<Route[]> {
    return this.http.get<Route[]>(this.baseUrl + 'api/Route/list/' + index);
  }

  getRestriction() : Observable<Restrictions> {
    return this.http.get<Restrictions>(this.baseUrl + 'api/Route/restrictions');
  }



  // createRecipe(recipe: Recipe): Observable<Recipe> {
  //   return this.http.post<Recipe>(this.baseUrl + '/api/Recipe/save', recipe);
  // }
  //
  // getRecipeList(index: Number): Observable<Recipe[]>{
  //   return this.http.get<Recipe[]>(this.baseUrl + '/api/Recipe/list/' + index);
  // }
  //
  // updatePhoto(url: string, file: FormData) : Observable<Recipe> {
  //   return this.http.post<Recipe>(url, file);
  // }
  //
  // getRecipe(index: Number): Observable<Recipe> {
  //    return this.http.get<Recipe>(this.baseUrl + '/api/Recipe/' + index);
  // }
  //
  // deleteRecipe(index: Number): Observable<String> {
  //   return this.http.delete<String>(this.baseUrl + '/api/Recipe/' + index + '/delete');
  // }
}
