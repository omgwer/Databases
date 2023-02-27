import {Component, OnInit} from '@angular/core';
import {RouteHelper} from "../data/helper/route.helper";
import {Route} from "../data/container/route.interface";
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Restrictions} from "../data/container/restrictions.interface";

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
    placement : [],
    localityName : [],
    busStopName : [],
    isHavePavilion : []
  };

  ngOnInit() {
    this.getRestrictions();
    this.getRouteList(this.listIndex);
  }

  constructor(private routeHelper: RouteHelper) {
    this.listIndex = 0;
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
    this.routeHelper.getRouteList(this.listIndex).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
    this.listIndex += 10;
  }

  getNextElements() : void {
    this.routeHelper.getRouteList(10).subscribe((x: Route[]) => {
      x.forEach((e) => this.routeList.push(e));
    })
    this.listIndex += 10;
  }

  getRestrictions() : void {
    this.routeHelper.getRestriction().subscribe((x: Restrictions) => {
      this.restrictions.localityName.push(this.dontSelect);
      this.restrictions.placement.push(this.dontSelect);
      this.restrictions.busStopName.push(this.dontSelect);
      this.restrictions.isHavePavilion.push(this.dontSelect);
      x.placement.forEach( e => this.restrictions.placement.push(e));
      x.localityName.forEach(e => this.restrictions.localityName.push(e))
      x.isHavePavilion.forEach(e => {
        if (e=== null)
          this.restrictions.isHavePavilion.push("Не указано")
        else
          this.restrictions.isHavePavilion.push(e)
      })
      x.busStopName.forEach(e => this.restrictions.busStopName.push(e))
    })
  }

  searchRoute() {
    let formData = { ...this.searchRouteForm.value };
    this.listIndex = 0;
    this.routeList = [];
  }
}
