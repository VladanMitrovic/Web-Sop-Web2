import React from 'react';
import { auth } from '../firebase';
import { FacebookAuthProvider,signInWithPopup } from "firebase/auth";


const SocialMediaRegistration = () => {
  const handleSocialMediaLogin = async (providerName) => {
    let provider;

    if (providerName === 'facebook') {
      provider = new FacebookAuthProvider();
    }

    try {
      await signInWithPopup(auth,provider);
      // Handle successful authentication response here
    } catch (error) {
      console.log(error);
      // Handle error here
    }
  };

  return (
    <div>
      <button className="btn btn-primary" onClick={() => handleSocialMediaLogin('facebook')}>
        Sign in with Facebook
      </button>

    </div>
  );
};

export default SocialMediaRegistration;