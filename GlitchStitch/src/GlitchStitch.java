/**
 * Stitch multiple Glitch playr sprite sheets into one.
 * 
 * @author J. Hollingsworth and G. Roth
 */

import java.awt.Graphics;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import javax.imageio.ImageIO;


public class GlitchStitch {
	
	private static final String[] FILE_NAMES = {"demon_base.png", 
		"demon_climb.png", "demon_jump.png", "demon_sleep.png"};
	private static final int[] FRAMES_WIDE = {15, 19, 33, 21};
	private static final int[] FRAMES_TALL = {1, 1, 1, 2};

	public static void main(String[] args) throws IOException {
			
		// how big is a frame?
		int frameWidth = 0;
		int frameHeight = 0;
		BufferedImage image[] = new BufferedImage[FILE_NAMES.length];
		for (int i = 0; i < FILE_NAMES.length; i++) {
			image[i] = ImageIO.read(new File(FILE_NAMES[i]));
			if (frameWidth < (image[i].getWidth() / FRAMES_WIDE[i]))
				frameWidth = image[i].getWidth() / FRAMES_WIDE[i];
			if (frameHeight < (image[i].getHeight() / FRAMES_TALL[i])) 
				frameHeight = image[i].getHeight() / FRAMES_TALL[i];
		}	
		System.out.println("frame size = (" + frameWidth + ", " + frameHeight + ")");
		
		// how big is the final sprite sheet?
		int fullWidth = (4096 / frameWidth) * frameWidth;
		int fullHeight = 0;
		for (int i = 0; i < FILE_NAMES.length; i++) {
			fullHeight += frameHeight * (Math.ceil((FRAMES_WIDE[i] * FRAMES_TALL[i] * frameWidth) / fullWidth) + 1);
		}
		
		System.out.println("Creating new file of size ("+ fullWidth + ", " + fullHeight + ")");
		
		BufferedImage result = new BufferedImage(
				fullWidth, fullHeight,
				BufferedImage.TYPE_INT_ARGB);
		Graphics g = result.getGraphics();

		int row = 0;
		int col = 0;
		for (int i = 0; i < FILE_NAMES.length; i++) {
			int width = image[i].getWidth() / FRAMES_WIDE[i];
			int height = image[i].getHeight() / FRAMES_TALL[i];
			System.out.print(FILE_NAMES[i] + ": (" + row + ", " + col + ") ==> ");
			for (int y = 0; y < FRAMES_TALL[i]; y++) {
				for (int x = 0; x < FRAMES_WIDE[i]; x++) {
					
					if (col >= fullWidth/frameWidth) {
						row++;
						col = 0;
					}
					
					g.drawImage(image[i].getSubimage(x * width, y * height, width, height),
							(col * frameWidth) + (frameWidth - width)/2,
							(row * frameHeight) + (frameHeight - height)/2,
							null);
					
					col++;					
				}
			}
			System.out.println("(" + row + ", " + (col - 1) + ")");
			row++;
			col = 0;
		}

		ImageIO.write(result, "png", new File("result.png"));
	}
}
