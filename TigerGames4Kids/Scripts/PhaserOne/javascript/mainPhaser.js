import { AUTO, Game } from '../javascript/libraries/phaser';

var config = {
    type: AUTO,
    width: 800,
    height: 600,
    scene: {
        preload: preload,
        create: create,
        update: update
    }
};

var button;
var music;
var cursors;
var scoreText;
var score = 0;
var clickSound;

var game = new Game(config);

function preload() {
    //load all assets
    this.load.image('sky', 'javascript/assets/sky.png');
    this.load.spritesheet('button',
        'javascript/assets/button-sheet.png',
        { frameWidth: 416, frameHeight: 419 }
    );

    //firefox does not load mp3 for some reason so ogg will be used if mp3 does not work
    this.load.audio('piano', ['javascript/assets/kirby_loop.mp3', 'javascript/assets/kirby_loop.ogg']);
    this.load.audio('click', ['javascript/assets/button-click.mp3', 'javascript/assets/button-click.ogg']);
}

function create() {
    //add background
    this.add.image(400, 300, 'sky');

    //add music
    clickSound = this.sound.add('click');
    music = this.sound.add('piano');
    music.loop = true;
    music.play(); 

    //add button sprite
    button = this.add.sprite(400, 300, 'button').setInteractive();
    
    //create cursor object for button input
    cursors = this.input.keyboard.createCursorKeys();
   
    //button animation
    this.anims.create({
        key: 'off',
        frames: this.anims.generateFrameNumbers('button', { start: 0, end: 0 }),
        frameRate: 20,
        repeat: -1
    });
    this.anims.create({
        key: 'on',
        frames: this.anims.generateFrameNumbers('button', { start: 1, end: 1 }),
        frameRate: 20,
        repeat: -1
    });

    scoreText = this.add.text(16, 16, 'clicks: 0', { fontSize: '32px', fill: '#000' });

    //action on click
    button.on('pointerdown', function () {
        button.anims.play('on', true);
        score += 1;
        scoreText.setText('Score: ' + score);
        clickSound.play();
    });
    button.on('pointerup', function () {
        button.anims.play('off', true);
    });
}
/*This function runs on a loop, be careful!*/
function update() {


}


 
