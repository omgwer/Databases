import {Component, OnInit} from '@angular/core';
import {RouteHelper} from "../data/helper/route.helper";
import {Route} from "../data/container/route.interface";
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Restrictions} from "../data/container/restrictions.interface";
import {SearchSubstringRequest} from "../data/container/searchSubstringRequest.interface";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  listIndex: number;
  dontSelect: string = "не выбрано";
  routeList: Route[] = [];
  restrictions: Restrictions = {
    placement: [],
    localityName: [],
    busStopName: [],
    isHavePavilion: [],
    minRange: 0.0,
    maxRange: 0.0,
    orderBy: ""
  };


  ngOnInit() {
    this.getRestrictions();
    this.getRouteList(this.listIndex);
  }

  constructor(private routeHelper: RouteHelper) {
    this.listIndex = 0;
  }

  searchSubstringForm: FormGroup = new FormGroup({
    searchSubstringInput: new FormControl(this.dontSelect)
  });

  searchRouteForm: FormGroup = new FormGroup({
    startPoint: new FormControl(this.dontSelect),
    finishPoint: new FormControl(this.dontSelect),
    rangeLeft: new FormControl(this.dontSelect),
    rangeRight: new FormControl(this.dontSelect),
    busStopName: new FormControl(this.dontSelect),
    placementAlongTheRoad: new FormControl(this.dontSelect),
    isHavePavilion: new FormControl(this.dontSelect)
  });

  getRouteList(index: Number): void {
    this.clearSearchResult();
    this.routeHelper.getRouteList(this.listIndex).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
    this.listIndex += 10;
  }

  getNextElements(): void {
    this.routeHelper.getRouteList(10).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
    this.listIndex += 10;
  }

  getRestrictions(): void {
    this.routeHelper.getRestriction().subscribe((x: Restrictions) => {
      this.restrictions.localityName.push(this.dontSelect);
      this.restrictions.placement.push(this.dontSelect);
      this.restrictions.busStopName.push(this.dontSelect);
      this.restrictions.isHavePavilion.push(this.dontSelect);
      this.restrictions.minRange = 0.0;
      this.restrictions.maxRange = 0.0;
      x.placement.forEach(e => this.restrictions.placement.push(e));
      x.localityName.forEach(e => this.restrictions.localityName.push(e))
      x.isHavePavilion.forEach(e => {
        if (e === null)
          this.restrictions.isHavePavilion.push("Не указано")
        else
          this.restrictions.isHavePavilion.push(e)
      })
      x.busStopName.forEach(e => this.restrictions.busStopName.push(e))
    })
  }

  clearSearchForm() {
    this.searchRouteForm.reset();
  }

  clearSearchResult() {
    this.listIndex = 0;
    this.routeList = [];
  }

  searchRoute() {
    let formData = {...this.searchRouteForm.value};
    this.clearSearchResult();
  }

  searchSubstring() {
    this.clearSearchResult();
    let searchString = this.searchSubstringForm.value.searchSubstringInput;
    if (searchString == "")
      return;
    let searchSubstring: SearchSubstringRequest = {
      substring: searchString,
      limit: 10,
      offset: this.listIndex
    };

    console.log(searchSubstring.substring);

    this.routeHelper.searchSubstring(searchSubstring).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
    this.listIndex += 10;
  }

  toDto(): void {
  }

}


