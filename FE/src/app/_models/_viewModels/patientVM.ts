import { Address } from "../address";
import { PersonName } from "../personName";

export class PatientVM {
	civilRegistrationNumber: string;
	civilStatusCode: string;
	civilSubstituteNumberNational: string;
	countryIdentificationCode: string;
	countryIdentificationCodeSST: string;
	countryIdentificationText: string;
	parishDistrictCode: string;
	parishDistrictText: string;
	personGenderCode: string;
	populationDistrictCode: string;
	populationDistrictText: string;
	practitionerIdentificationCode: string;
	regionalCode: string;
	regionalName: string;
	socialDistrictCode: string;
	socialDistrictText: string;
	statusCode: string;
    
    address: Address = new Address();
    patientName: PersonName = new PersonName();
}