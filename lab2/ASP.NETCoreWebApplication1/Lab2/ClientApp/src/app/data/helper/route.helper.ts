import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Route} from "../container/route.interface";
import {Restrictions} from "../container/restrictions.interface";
import {SearchSubstringRequest} from "../container/searchSubstringRequest.interface";
import {SearchParameters} from "../container/searchParameters.interface";

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

  getRouteList(searchParameters: SearchParameters): Observable<Route[]> {
    return this.http.post<Route[]>(this.baseUrl + 'api/Route/list/search', searchParameters);
  }

  getRestriction() : Observable<Restrictions> {
    return this.http.get<Restrictions>(this.baseUrl + 'api/Route/restrictions');
  }

  searchSubstring(request: SearchSubstringRequest) : Observable<Route[]> {
    return this.http.post<Route[]>(this.baseUrl + 'api/Route/searchSubstring', request );
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
