class Car{
    constructor(name, model, manufacturer, yearOfManufacture, price, lastServiceDate){
        this.name = name;
        this.model = model;
        this.manufacturer = manufacturer;
        this.yearOfManufacture = yearOfManufacture;
        this.price = price;
        this.lastServiceDate = lastServiceDate;
    }
}

let array = [
    new Car('Z8', 'Manual', 'BMW', 2017, 2300.09, new Date(2021, 01, 14)),
    new Car('A6', 'CVT', 'Audi', 2014, 23540.09, new Date(2021, 01, 17)),
    new Car('AMG', 'DCT', 'Mercedes', 2019, 567500.09, new Date(2020, 02, 14)),
    new Car('Tiago', 'CVT', 'Tata', 2020, 76850.09, new Date(2019, 11, 14)),
    new Car('Nexon', 'Manual', 'Tata', 2016, 2308500.09, new Date(2020, 09, 30)),
    new Car('i20', 'Manual', 'Hyundai', 2014, 2308500.09, new Date(2018, 07, 04)),
    new Car('Baleno', 'CVT', 'Suzuki', 2011, 245245.09, new Date(2020, 02, 14)),
    new Car('Ertiga', 'IMT', 'Suzuki', 2017, 3434533.09, new Date(2020, 09, 30)),
    new Car('S class', 'DCT', 'Mercedes', 2012, 3453453.09, new Date(2018, 07, 04)),
    new Car('Bolero', 'Manual', 'Mahindra', 2015, 23423422.09, new Date(2019, 11, 14)),
    new Car('Kwid', 'CVT', 'Renault', 2016, 6786785.09, new Date(2021, 01, 17)),
    new Car('Seltos', 'Manual', 'Kia', 2013, 478326.09, new Date(2018, 07, 04)),
    new Car('Sonet', 'DCT', 'Kia', 2020, 7624773.09, new Date(2021, 01, 17)),
    new Car('Venue', 'IMT', 'Hyundai', 2020, 6762346.09, new Date(2020, 09, 30)),

];

console.log(array);
console.log(array.length);
console.log(array.filter(car => car.name == 'A6'));
console.log(array.filter(car => car.model == 'CVT'));
console.log(array.filter(car => car.manufacturer == 'Suzuki'));
console.log(array.filter(car => car.yearOfManufacture < 2018 ));
console.log(array.filter(car => car.price < 50000));

var date = new Date();
console.log(date.toLocaleDateString());

date.setMonth(date.getMonth()-1);
console.log(date.toLocaleDateString());

date = new Date();
date.setMonth(date.getMonth()+2, 0);
console.log(date.toLocaleDateString());