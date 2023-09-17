import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";


const firebaseConfig = {

  apiKey: "AIzaSyDMigVUTfYyRTwhCIK6TQK3IFr9SoRCV0U",
  authDomain: "web2projekat-f12f3.firebaseapp.com",
  projectId: "web2projekat-f12f3",
  storageBucket: "web2projekat-f12f3.appspot.com",
  messagingSenderId: "32222060841",
  appId: "1:32222060841:web:40fae036ec3a699fc91738",
  measurementId: "G-26DDHZ63D9"
  
  }

const app = initializeApp(firebaseConfig);

export const auth = getAuth(app);



