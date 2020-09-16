import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  myForm: FormGroup;
  imageSrc: string;
  constructor(private route: ActivatedRoute, private apiService: ApiService, private fb: FormBuilder) {

  }

  ngOnInit(): void {
    this.myForm = this.fb.group({
      img: [''],
      name: [''],
      description: [''],
      price: ['']
    });

    this.route.params.subscribe(param => {      
      console.log('param', param.id);
      this.apiService.getInventoryById(param.id).subscribe((result) =>{      
        console.log('resp', result);
        var resp = result.value;
        this.myForm.patchValue({ 'name':resp.name});
        this.myForm.patchValue({ 'description':resp.description});
        this.myForm.patchValue({ 'price':resp.price});
       // this.myForm.patchValue({ 'img':resp.image});
        this.imageSrc = `data:image/gif;base64,${resp.image}`
      });

    });
  }

}
