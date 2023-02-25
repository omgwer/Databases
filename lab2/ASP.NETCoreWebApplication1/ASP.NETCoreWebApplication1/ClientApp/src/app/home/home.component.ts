import {Component, OnInit} from '@angular/core';
import {RouteHelper} from "../data/helper/route.helper";
import {Route} from "../data/container/route.interface";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  listIndex: number;
  routeList: Route[] = [];

  ngOnInit() {
    this.getRouteList(0);
  }

  constructor(private routeHelper: RouteHelper) {
    this.listIndex = 0;
  }

  getRouteList(index: Number) : void {
    // this.routeHelper.getRouteList(0).subscribe((x: Route[]) => {
    //   x.forEach((e) => this.routeList.push(e));
    //   this.listIndex++;
    // })


    let res =  this.routeHelper.getRouteList(0);
    res.forEach(e => {
      let test: Route = {
        placementAlongTheRoad : e
      }
      this.routeList.push(test);
    })
  }
}
