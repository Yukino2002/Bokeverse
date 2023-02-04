import React from 'react';
import Image from 'next/image';

function Feature({ image, title, description }) {
    return (
        <div className='flex flex-row gap-4'>
            <Image src={image} alt="image" width={125} />
            <div className='flex flex-col gap-2.5'>
                <div>
                    <h3 className="font-serif text-xl">
                        {title}
                    </h3>
                </div>
                <div>
                    <p className="font-serif">
                        {description}
                    </p>
                </div>
            </div>
        </div>
    );
}

export default Feature;