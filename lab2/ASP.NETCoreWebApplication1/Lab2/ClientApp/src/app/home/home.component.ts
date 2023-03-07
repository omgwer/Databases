import {Component, OnInit} from '@angular/core';
import {RouteHelper} from "../data/helper/route.helper";
import {Route} from "../data/container/route.interface";
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Restrictions} from "../data/container/restrictions.interface";
import {SearchSubstringRequest} from "../data/container/searchSubstringRequest.interface";
import {SearchParameters} from "../data/container/searchParameters.interface";

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
    maxRange: 0.0
  };

  search: SearchParameters = {
    offset : 0,
    busStopName : null,
    maxRange : null,
    minRange : null,
    finishPoint : null,
    isHavePavilion : null,
    startPoint : null,
    order : null,
    direction : null
  }

  // direction ASC && DESC

  ngOnInit() {
    this.getRestrictions();
    this.getRouteList();
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
    minRange: new FormControl(this.dontSelect),
    maxRange: new FormControl(this.dontSelect),
    busStopName: new FormControl(this.dontSelect),
    placement: new FormControl(this.dontSelect),
    isHavePavilion: new FormControl(this.dontSelect)
  });

  getRouteList(): void {
    this.clearSearchResult();
    this.routeHelper.getRouteList(this.search).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
  }

  getNextElements(): void {
    this.search.offset += 10;
    console.log(this.search);
    this.routeHelper.getRouteList(this.search).subscribe((x: Route[]) => {
      console.log(x);
      x.forEach((e) => this.routeList.push(e));
    })
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
    this.search.offset = 0;
    this.listIndex = 0;
    this.routeList = [];
  }

  searchRoute() {
    this.clearSearchResult();
    this.search = {
      offset : 0,
      busStopName : null,
      maxRange : null,
      minRange : null,
      finishPoint : null,
      isHavePavilion : null,
      startPoint : null,
      order : null,
      direction : null
    }
    this.search.offset = 0;
    let formData = {...this.searchRouteForm.value};
    if (formData.startPoint != this.dontSelect) {
      this.search.startPoint = formData.startPoint;
    }
    if (formData.finishPoint != this.dontSelect) {
      this.search.finishPoint = formData.finishPoint;
    }
    if (formData.minRange != this.dontSelect && !isNaN(parseFloat(formData.minRange))) {
      this.search.minRange = formData.minRange;
    } else {
      console.log("minRange is not valid");
    }
    if (formData.maxRange != this.dontSelect && !isNaN(parseFloat(formData.maxRange))) {
      this.search.maxRange = formData.maxRange;
    } else {
      console.log("maxRange is not valid");
    }
    if (formData.busStopName != this.dontSelect) {
      this.search.busStopName = formData.busStopName;
    }
    if (formData.isHavePavilion != this.dontSelect) {
      this.search.isHavePavilion = formData.isHavePavilion;
    }
    this.routeHelper.getRouteList(this.search).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
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

    this.routeHelper.searchSubstring(searchSubstring).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
  }

  orderList(index: number) {
    let some = document.getElementsByClassName("pointer");
    let  desc = "DESC";
    let  asc = "ASC";
    let direction = asc;
    this.routeList = [];



    if ( some[index - 1].classList.contains("rotate")) {
      direction = desc;
    }
    else {
      direction = asc;
    }
    // some[0].classList.remove("rotate")
    // some[1].classList.remove("rotate")
    // some[2].classList.remove("rotate")
    // some[3].classList.remove("rotate")
    // some[4].classList.remove("rotate")
    // some[5].classList.remove("rotate")

    some[index - 1].classList.toggle("rotate");

    switch (index) {
      case 1 : {
        this.search.order = "startPoint";
        break;
      }
      case 2 : {
        this.search.order = "finishPoint";
        break;
      }
      case 3 : {
        this.search.order  ="range"
        break;
      }
      case 4 : {
        this.search.order  ="busStopName"
        break;
      }
      case 5 : {
        this.search.order  ="placementAlongTheRoad"
        break;
      }
      case 6 : {
        this.search.order  ="isHavePavilion"
        break;
      }
    }
    this.search.direction = direction;
    console.log(this.search);
    this.routeHelper.getRouteList(this.search).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
  }

}


