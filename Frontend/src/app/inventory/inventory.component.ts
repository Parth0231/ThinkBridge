import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  inventories = [];
  imageSrc: string;
  buttonname: string;
  myForm: FormGroup;
  selectedFile: File;
  inventid: any;
  private base64textString: String = "";
  //data = [{ "id": 1, "name": "toolkit", "description": "tool kit is used for work", "image": null, "price": 10.00 }, { "id": 2, "name": "water bottle updated", "description": "to drink water updated", "image": null, "price": 12.00 }];
  constructor(private apiService: ApiService, private fb: FormBuilder, private router: Router,private domSanitizer: DomSanitizer) {

  }

  ngOnInit() {
    this.apiService.getInventories().subscribe((data: any) => {
      data.value.forEach(element => {
        element.image = this.domSanitizer.bypassSecurityTrustUrl('data:image/png;base64, '+ element.image) ;
      });
      this.inventories = data.value;
    });

    this.myForm = this.fb.group({
      img: [''],
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.minLength(2)]]
    });
    this.buttonname = "Submit";
    console.log(this.myForm);
  }

  onSubmit(formvalue: any) {


    debugger;
    if (this.buttonname === 'Submit') {

      var params = {
        "id": 0,
        'name': formvalue.name,
        'description': formvalue.description,
        'image': this.base64textString,
        'price': formvalue.price,
      };
      this.apiService.postInventory(params).subscribe((resp) => {
        if (resp !== null) {
          window.alert("Inventory saved successfully");
          this.apiService.getInventories().subscribe((data: any) => { 
            data.value.forEach(element => {
              element.image = this.domSanitizer.bypassSecurityTrustUrl('data:image/png;base64, '+ element.image) ;
            });        
            this.inventories =data.value;
          });
          this.myForm.reset();
          this.imageSrc = '';
        }
      });
    } else {
      debugger;
      var params1 = {
        "id": this.inventid,
        'name': formvalue.name,
        'description': formvalue.description,
        'image': this.base64textString,
        'price': formvalue.price,
      };
      this.apiService.updateInventory(params1).subscribe((resp) => {
          window.alert("Inventory updated successfully");
          this.apiService.getInventories().subscribe((data: any) => {
            data.value.forEach(element => {
              element.image = this.domSanitizer.bypassSecurityTrustUrl('data:image/png;base64, '+ element.image) ;
            });
            this.inventories =data.value;
          });
          this.myForm.reset();
          this.imageSrc = '';
          this.buttonname = 'Submit';
       
      });
    }
  }

  handleFileSelect(evt) {
    var files = evt.target.files;
    var file = files[0];
    this.selectedFile = evt.target.files[0]
    if (files && file) {
      var reader = new FileReader();
      var reader1 = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
      reader1.readAsDataURL(file);
      reader1.onload = () => {
        this.imageSrc = reader1.result as string;
        this.myForm.patchValue({
          fileSource: reader1.result
        });
      };
    }
  }

  _handleReaderLoaded(readerEvt) {
    var binaryString = readerEvt.target.result;
    this.base64textString = btoa(binaryString);
    console.log(btoa(binaryString));
  }

  onFileChange(event) {
    debugger;
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      this.selectedFile = event.target.files[0]
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.myForm.patchValue({
          fileSource: reader.result
        });

      };

    }
  }

  get f() {
    return this.myForm.controls;
  }

  GetInventory(id) {
    this.router.navigate([`productdetails/${id}`]);
  }

  Delete(id) {
    this.apiService.deleteInventory(id).subscribe((resp) => {     
        window.alert("Inventory deleted successfully");
        this.apiService.getInventories().subscribe((data: any) => {
          data.value.forEach(element => {
            element.image = this.domSanitizer.bypassSecurityTrustUrl('data:image/png;base64, '+ element.image) ;
          });
          this.inventories = data.value;
        });
      
    });
  }

  Edit(id) {
    this.apiService.getInventoryById(id).subscribe((resp) => {
      console.log('resp', resp);
      var result = resp.value;
      this.inventid = result.id;
      this.myForm.patchValue({ 'name': result.name });
      this.myForm.patchValue({ 'description': result.description });
      this.myForm.patchValue({ 'price': result.price });
      // this.myForm.patchValue({ 'img':resp.image});
      this.imageSrc = `data:image/gif;base64,${result.image}`;
      this.base64textString = result.image;
    });
    this.buttonname = 'Update';
  }
}
