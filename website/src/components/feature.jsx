import React from 'react';
import Image from 'next/image';

function Feature({ image, title, description }) {
    return (
        <div className='flex flex-row gap-4 w-[50%]'>
            <Image src={image} alt="image" width={135} />
            <div className='flex flex-col gap-2.5'>
                <div>
                    <h3 className="font-boogaloo text-3xl">
                        {title}
                    </h3>
                </div>
                <div>
                    <p className="font-serif text-[#82818A] text-lg">
                        {description}
                    </p>
                </div>
            </div>
        </div>
    );
}

export default Feature;