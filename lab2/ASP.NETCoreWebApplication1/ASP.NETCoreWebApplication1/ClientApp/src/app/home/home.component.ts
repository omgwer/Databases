import {Component, OnInit} from '@angular/core';
import {RouteHelper} from "../data/helper/route.helper";
import {Route} from "../data/container/route.interface";
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  listIndex: number;
  routeList: Route[] = [];

  ngOnInit() {
    this.getRouteList(0);
  }

  constructor(private routeHelper: RouteHelper) {
    this.listIndex = 0;
    let init: Route  = {
      startPoint: "не выбрано",
      finishPoint: "не выбрано",
      rangeFromStart: 0,
      busStopName: "не выбрано",
      placementAlongTheRoad : "не выбрано",
      isHavePavilion: "не выбрано"
    };
    this.routeList.push(init);
  }

  searchRouteForm: FormGroup = new FormGroup({
    startPoint: new FormControl('не выбрано'),
    finishPoint: new FormControl('не выбрано'),
    rangeLeft: new FormControl('не выбрано'),
    rangeRight: new FormControl('не выбрано'),
    busStopName: new FormControl('не выбрано'),
    placementAlongTheRoad: new FormControl('не выбрано'),
    isHavePavilion: new FormControl('не выбрано')
  });

  getRouteList(index: Number) : void {
    this.routeHelper.getRouteList(0).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
  }

  searchRoute() {
    let formData = { ...this.searchRouteForm.value };
    console.log(formData);
  }
}
